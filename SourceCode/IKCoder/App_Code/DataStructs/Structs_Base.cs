using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

public class Structs_Base
{
	public Structs_Base()
	{
		
	}

    public List<string> GetSetOnlyPropertiesLst()
    {
        List<string> resultLst = new List<string>();
        PropertyInfo[] propertiesResult = this.GetType().GetProperties(BindingFlags.SetProperty);
        foreach(PropertyInfo activeProperty in propertiesResult)        
            resultLst.Add(activeProperty.Name);
        return resultLst;
    }

    public List<string> GetGetOnlyPropertiesLst()
    {
        List<string> resultLst = new List<string>();
        PropertyInfo[] propertiesResult = this.GetType().GetProperties(BindingFlags.GetProperty);
        foreach (PropertyInfo activeProperty in propertiesResult)
            resultLst.Add(activeProperty.Name);
        return resultLst;
    }

    public object GetPropertyValue(string properName)
    {
        return this.GetType().GetProperty(properName).GetValue(this,null);
    }

}