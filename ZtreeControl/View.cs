using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZtreeControl
{
    class View
    {
        public ArrayList MainServerFormMenuAddIns()
        {
            return new ArrayList();
        }

        public ArrayList ClientsTableMenuAddIns()
        {
            return new ArrayList();
        }
    }
}

/*
 * 
 * // Check if user has access to requested .dll.
string strDllPath = Path.GetFullPath(strSomePath);
if (File.Exists(strDllPath))
{
    // Execute the method from the requested .dll using reflection (System.Reflection).
    Assembly DLL = Assembly.LoadFrom(strDllPath);
    Type classType = DLL.GetType(String.Format("{0}.{1}", strNmSpaceNm, strClassNm));
    if (classType != null)
    {
        // Create class instance.
        classInst = Activator.CreateInstance(classType);

        // Invoke required method.
        MethodInfo methodInfo = classType.GetMethod(strMethodName);
        if (methodInfo != null)
        {
            object result = null;
            result = methodInfo.Invoke(classInst, new object[] { dllParams });
            return result.ToString();
        }
    }
}
 * 
 */
