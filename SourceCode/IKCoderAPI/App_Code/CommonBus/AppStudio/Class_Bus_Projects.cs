using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class_Bus_ProjectDoc
/// </summary>
public class Class_Bus_Projects
{

    class_CommonData refObjectCommonData;
    class_Bus_ProfileDocs refObjectProfileDocs;

    Class_Bus_Projects(ref class_CommonData refActiveCommonDataObject,ref class_Bus_ProfileDocs refActiveProfileDocs)
    {
        refObjectCommonData = refActiveCommonDataObject;
        refObjectProfileDocs = refActiveProfileDocs;
    }

    
 
}