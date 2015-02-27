using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace fair_energy_sharing.EnergyAssigner
{
    public static class AssignerFactory
    {
        public static IAssigner CreateAssigner(string assignerType) {

            // get the type of the interface
            var type = typeof(IAssigner);
            // query all the classes that implement IMyInterface in current assembly
            // startTime.IsClass is very important, otherwise you will get more interface types 
            // that inherited IMyInterface, include IMyInterface itself
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && type.IsAssignableFrom(t)
                    select t;
            // convert the query result to list (List<type>)
            var classes = q.ToList();
            // find the one we need
            // var myClass = classes.FirstOrDefault(c => c.Name.ToLower().Contains(arrivalType.ToString().ToLower()));

            var myClass = classes.FirstOrDefault(c => c.Name.Equals(assignerType));
            // create an instance from a type
            var instance = (IAssigner)Activator.CreateInstance(myClass);
            // return the created instance
            return instance;
        }
    }
}
