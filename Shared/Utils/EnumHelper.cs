using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementBlazor.Shared.Utils
{
    public class EnumHelper
    {
        public static List<DropDownItem> EnumToList<T>(string intialValue, string intialText)
        {
            var list = new List<DropDownItem>();
            if (!string.IsNullOrEmpty(intialText) && !string.IsNullOrEmpty(intialValue))
            {
                DropDownItem ddi = new DropDownItem { 
                Text = intialText,
                Value = intialValue
                };
                list.Add(ddi);
            }

            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            for (int i = 0; i < Enum.GetNames(typeof(T)).Length; i++)
            {
                DropDownItem ddi = new DropDownItem
                {
                    Text = Enum.GetNames(typeof(T))[i],
                    Value = values[i].ToString()
                };
                list.Add(ddi);
            }
            return list;
        }
    }
}
