using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ReflectionController
    {

        //Lấy thông tin controller
        public List<Type> GetController(string namespaces)
        {
            List<Type> listController = new List<Type>();
            Assembly assmebly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assmebly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces)).OrderBy(x => x.Name);

            listController = types.ToList();
            return listController;
        }

        //Lấy thông tin action của các Controller

        public List<string> GetActions(Type Controller)
        {
            List<string> listAction = new List<string>();
            IEnumerable<MemberInfo> memberInfo = Controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly |
                BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).OrderBy(x => x.Name);

            foreach (MemberInfo method in memberInfo)
            {
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute)))
                {
                    listAction.Add(method.Name.ToString());
                }
            }
            return listAction;
        }
    }

}