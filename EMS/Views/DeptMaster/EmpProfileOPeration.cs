using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS;
using EMS.Models;
namespace BusinessLayer
{
    public class EmpProfileOperations
    {
        EmpProfileRepository empProfileRepository = new EmpProfileRepository(new EmsContext());
        public void insertEmpProfile(EmpProfile empProfile)
        {
            empProfileRepository.Insert(empProfile);
        }
        public List<EmpProfile> ListOfEmployee()
        {
            return empProfileRepository.GetAll().ToList();
        }
    }
}