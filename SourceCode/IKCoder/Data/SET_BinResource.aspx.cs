using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iKCoder_Platform_SDK_Kit;

public partial class Data_SET_BinResource : class_WebBase_NUA
{
    protected override void ExtendedAction()
    {
        ISRESPONSEDOC = true;
        byte[] binBuffer = new byte[Request.InputStream.Length];
        Request.InputStream.Read(binBuffer, 0, (int)Request.InputStream.Length);
    }
}