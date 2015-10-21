<%@ WebHandler Language="C#" Class="LoginHandler" %>

using System;
using System.Web;

using System.Xml;//for XmlReader
using System.Text;//for  Encoding
public class LoginHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //上传的字符串长度
        int length = context.Request.ContentLength;
        //读取上传字符的二进制到字节数组
        byte[] bt = context.Request.BinaryRead(length);
        //二进制字节数组转换为字符串
        string upstring = Encoding.UTF8.GetString(bt);
        //设置返回信息格式，文本
        context.Response.ContentType = "text/plain";
        XmlReader readXml = XmlReader.Create(context.Server.MapPath("~/loginXML/zhuce.xml"));
        while (readXml.Read())
        {
            if (readXml.GetAttribute("password") == upstring)
            {
                context.Response.Write("Yes");
                break;
            }
        }
        readXml.Close();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}