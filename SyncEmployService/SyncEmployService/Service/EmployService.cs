using SyncEmployService.IRepository;
using SyncEmployService.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using SyncEmployService.Common.Helper;
using SyncEmployService.Models.View;
using SyncEmployService.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using SyncEmployService.Models.Dto;
using System.Threading;
using System.Collections.Concurrent;
using SyncEmployService.Models.Entity;

namespace SyncEmployService.Service
{
    public class EmployService : IEmployService
    {
        private readonly IEmployRepository _employRepository;
        private readonly INewDeptRepository _newDeptRepository;
        private readonly ILog _log;
        public EmployService(IEmployRepository employRepository,  INewDeptRepository newDeptRepository,ILog log)
        {
            _employRepository = employRepository;         
            _newDeptRepository = newDeptRepository;
            _log = log;
        }

        public async Task SyncEmployAsync()
        {
            try
            {
                string token = "";
                DateTime expiresIn = DateTime.Now;
                int totalPage = 1;
                int pageSize = 100;
                var tokenResponse = await GetTokenAsync();
                if (tokenResponse == null || tokenResponse.code != 0||tokenResponse.data==null)
                {
                    return;
                }
                token = tokenResponse.data.token;
                expiresIn = DateTime.Now.AddSeconds(tokenResponse.data.expiresIn).AddMinutes(-5);//提前五分钟过期重新获取token
                ConcurrentBag<EmployView> employViews = new ConcurrentBag<EmployView>();
                var firstEmployViews = await GetEmployViewsAsync(1, pageSize, token);
                if (firstEmployViews == null || firstEmployViews.code != 0 || firstEmployViews.data == null|| firstEmployViews.data.list == null)
                {
                    return;
                }
                totalPage = firstEmployViews.data.totalPage;
                for (int i = 0; i < firstEmployViews.data.list.Count; i++)
                {
                    employViews.Add(firstEmployViews.data.list[i]);
                }
                if (totalPage > 1)
                {
                    for (int i = 1; i < totalPage; i++)
                    {
                        var tempList = await GetEmployViewsAsync((int)i + 1, pageSize, token);
                        if (tempList != null && tempList.code == 0 && tempList.data != null && tempList.data.list != null)
                        {
                            foreach (var item in tempList.data.list)
                            {
                                employViews.Add(item);
                            }
                        }
                    }
                }
                List<EmployView> list = employViews.ToList();
                if (list.Count == 0)
                {
                    return;
                }
                var oldEmploys = await _employRepository.GetEmployeesAsync();
                List<NewDept> newDepts = await _newDeptRepository.GetNewDeptsAsync();
                List<EmployView> newEmployView = new List<EmployView>();
                List<EmployView> existedEmployView = new List<EmployView>();
                newEmployView = list.Where(x => oldEmploys.All(y => y.Name != x.employeeCode)).ToList();
                existedEmployView = list.Where(x => oldEmploys.Any(y => y.Name == x.employeeCode)).ToList();
                List<Employ> addEmploys = new List<Employ>();
                List<Employ> updateEmploys = new List<Employ>();
                List<NewDept> addNewDepts = new List<NewDept>();
                for (int i = 0; i < newEmployView.Count; i++)
                {
                    var newDept = newDepts.Where(x => x.DeptCode == newEmployView[i].unitCode).FirstOrDefault();
                    if (newDept == null && !addNewDepts.Any(x => x.DeptCode == newEmployView[i].unitCode))
                    {
                        addNewDepts.Add(new NewDept
                        {
                            IsExist = 0,
                            IsSalesMan = 0,
                            DeptCode = newEmployView[i].unitCode,
                            DName = newEmployView[i].unitName,
                            Guid = Guid.NewGuid()
                        });
                    }
                    DateTime? leaveTime = null;
                    if (DateTime.TryParse(newEmployView[i].gmtLeave, out DateTime tempLeaveTime))
                    {
                        leaveTime = tempLeaveTime;
                    }
                    DateTime? cStartTime = null;
                    if (DateTime.TryParse(newEmployView[i].dateOfJoin, out DateTime tempCStartTime))
                    {
                        cStartTime = tempCStartTime;
                    }
                    Employ employ = new Employ()
                    {
                        Name = newEmployView[i].employeeCode,
                        //Phone = newEmployView[i].phone,
                        Worked = newEmployView[i].hiringStatus == "Active" ? "在职" : "离职",
                        Dept = newEmployView[i].unitName,
                        OtherName = newEmployView[i].fullName,
                        LeaveTime = leaveTime,
                        CStartTime = cStartTime
                    };
                    addEmploys.Add(employ);
                }
                for (int i = 0; i < existedEmployView.Count; i++)
                {
                    var newDept = newDepts.Where(x => x.DeptCode == existedEmployView[i].unitCode).FirstOrDefault();
                    if (newDept == null && !addNewDepts.Any(x => x.DeptCode == existedEmployView[i].unitCode))
                    {
                        addNewDepts.Add(new NewDept
                        {
                            IsExist = 0,
                            IsSalesMan = 0,
                            DeptCode = existedEmployView[i].unitCode,
                            DName = existedEmployView[i].unitName,
                            Guid = Guid.NewGuid()
                        });
                    }
                    var oldEmploy = oldEmploys.Where(x => x.Name == existedEmployView[i].employeeCode).FirstOrDefault();
                    bool flag = false;
                    if (oldEmploy.OtherName != existedEmployView[i].fullName)
                    {
                        flag = true;
                        oldEmploy.OtherName = existedEmployView[i].fullName;
                    }
                    if (oldEmploy.Dept != existedEmployView[i].unitName)
                    {
                        flag = true;
                        oldEmploy.Dept = existedEmployView[i].unitName;
                    }
                    DateTime? leaveTime = null;
                    if (DateTime.TryParse(existedEmployView[i].gmtLeave, out DateTime tempLeaveTime))
                    {
                        leaveTime = tempLeaveTime;
                    }
                    DateTime? cStartTime = null;
                    if (DateTime.TryParse(existedEmployView[i].dateOfJoin, out DateTime tempCStartTime))
                    {
                        cStartTime = tempCStartTime;
                    }
                    if (oldEmploy.LeaveTime != leaveTime)
                    {
                        flag = true;
                        oldEmploy.LeaveTime = leaveTime;
                    }
                    if (oldEmploy.CStartTime != cStartTime)
                    {
                        flag = true;
                        oldEmploy.CStartTime = cStartTime;
                    }
                    if (oldEmploy.Worked == "在职" && existedEmployView[i].hiringStatus != "Active")
                    {
                        flag = true;
                        oldEmploy.Worked = "离职";
                    }
                    if (oldEmploy.Worked == "离职" && existedEmployView[i].hiringStatus != "Terminated")
                    {
                        flag |= true;
                        oldEmploy.Worked = "在职";
                    }
                    //if (oldEmploy.Phone != newEmployView[i].phone)
                    //{
                    //    flag = true;
                    //}
                    if (flag)
                    {
                        updateEmploys.Add(oldEmploy);
                    }
                }
                if (updateEmploys.Count > 0)
                {
                    int addCount = 100;
                    for (int i = 0; i < updateEmploys.Count; i = i + addCount)
                    {
                        int count = i + addCount < updateEmploys.Count ? addCount : updateEmploys.Count - i;
                        var tempList = updateEmploys.GetRange(i, count);
                        await _employRepository.UpdateEmployeesAsync(tempList);
                    }
                }
                if (addEmploys.Count > 0)
                {
                    int addCount = 100;
                    for (int i = 0; i < addEmploys.Count; i = i + addCount)
                    {
                        int count = i + addCount < addEmploys.Count ? addCount : addEmploys.Count - i;
                        var tempList = addEmploys.GetRange(i, count);
                        await _employRepository.InsertEmployeesAsync(tempList);
                    }
                }
                if (addNewDepts.Count > 0)
                {
                    int addCount = 100;
                    for (int i = 0; i < addNewDepts.Count; i = i + addCount)
                    {
                        int count = i + addCount < addNewDepts.Count ? addCount : addNewDepts.Count - i;
                        var tempList = addNewDepts.GetRange(i, count);
                        await _newDeptRepository.InsertNewDeptsAsync(tempList);
                    }
                }
                _log.Info("SyncEmployAsync Succsee");
            }
            catch (Exception ex)
            {
                _log.Error("SyncEmployAsync Error", ex);
            }

        }
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="nowPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<CommonResponse<Page<EmployView>>> GetEmployViewsAsync(int nowPageIndex, int pageSize, string token)
        {
            try
            {
                var client = new RestClient($"");
                EmployDto jsonBody = new EmployDto()
                {
                    employeeCodes = new List<string>(),
                    pageSize = pageSize,
                    nowPageIndex = nowPageIndex
                };
                var request = new RestRequest()
                    .AddJsonBody(jsonBody);
                request.AddHeader("Content-Type", "application/json");
                var response = await client.PostAsync<CommonResponse<Page<EmployView>>>(request);
                if (response == null || response.code != 0 || response.data == null)
                {
                    _log.Error("GetEmployViewsAsync 获取员工信息失败！");
                    return new CommonResponse<Page<EmployView>>()
                    {
                        code = -1,
                        success = false,
                        data = new Page<EmployView>()
                        {
                            list = new List<EmployView>(),
                            totalItem = 0,
                            totalPage = 0
                        }
                    };
                }
                return response;
            }
            catch (Exception ex)
            {
                _log.Error("GetEmployViewsAsync Error", ex);
                return new CommonResponse<Page<EmployView>>()
                {
                    code = -1,
                    success = false,
                    data = new Page<EmployView>()
                    {
                        list = new List<EmployView>(),
                        totalItem = 0,
                        totalPage = 0
                    }
                };
            }
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public async Task<CommonResponse<TokenView>> GetTokenAsync()
        {
            try
            {
                var client = new RestClient("");
                TokenDto jsonBody = new TokenDto()
                {
                    corpId = ConfigurationHelper.Instance.GetConfiguration(new string[] { "tokenInfo", "corpId" }),
                    appKey = ConfigurationHelper.Instance.GetConfiguration(new string[] { "tokenInfo", "appKey" }),
                    appSecret = ConfigurationHelper.Instance.GetConfiguration(new string[] { "tokenInfo", "appSecret" })
                };
                var request = new RestRequest().AddJsonBody(jsonBody);
                request.AddHeader("Content-Type", "application/json");
                var response = await client.PostAsync<CommonResponse<TokenView>>(request);
                if (response == null || response.code != 0 || response.data == null)
                {
                    _log.Error("GetTokenAsync 获取Token失败！");
                    return new CommonResponse<TokenView>()
                    {
                        code = -1,
                        success = false,
                        data = new TokenView() { expiresIn = -1, token = "" }
                    };
                }
                return response;
            }
            catch (Exception ex)
            {
                _log.Error("GetTokenAsync", ex);
                return new CommonResponse<TokenView>()
                {
                    code = -1,
                    success = false,
                    data = new TokenView() { expiresIn = -1, token = "" }
                };
            }
        }
    }
}
