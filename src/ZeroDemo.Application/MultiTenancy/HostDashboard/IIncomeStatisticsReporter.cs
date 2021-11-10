using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZeroDemo.MultiTenancy.HostDashboard.Dto;

namespace ZeroDemo.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}