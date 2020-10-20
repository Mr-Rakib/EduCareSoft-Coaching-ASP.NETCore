using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface IFeesCollection : ICRUD<FeesCollection, int , string>
    {
        List<FeesCollection> FindAllDiscount(string username, string month, int year);
        List<FeesCollection> FindAllPaidStudent(string username, string month, int year);
        List<FeesCollection> FindAllUnpaidStudent(string username, string month, int year);
        List<FeesCollection> FindAllHistory(string month, int year, string currentUsername);
        List<FeesCollection> FindMonthlyFeesByStudentId(int id, string currentUsername, string month, int year);
        List<FeesCollection> FindFeesCollectedByStaffId(int id, string currentUsername, string month, int year);
        string GenerateFeesForStudent(string month , int year , int institutionId, string currentUsername);
    }
}
