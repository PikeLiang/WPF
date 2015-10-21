<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;

using System.IO;//for FileStream
using System.Xml;//for XmlWriter
using System.Text;//for  Encoding
public class UploadHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //上传的字符串长度
        int length = context.Request.ContentLength;
        //读取上传字符的二进制到字节数组
        byte[] bt = context.Request.BinaryRead(length);
        //二进制字节数组转换为字符串
        string upstring = Encoding.UTF8.GetString(bt);  
        //从第1个字符开始截取字符串，直到"/"前
        string user = upstring.Substring(0, upstring.LastIndexOf("/"));
         //从"/"后开始截取字符串，直到尾
        string passw = upstring.Substring(upstring.LastIndexOf("/")+1);
        //以文件流形式打开文件写入
        FileStream filestream = File.OpenWrite(context.Server.MapPath("~/loginXML/zhuce.xml"));
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.OmitXmlDeclaration = true;//不写入XML声明
        // </renyuans>是11个字符，从尾部END倒退11个字符开始写入，原字符</renyuans>被覆盖
        filestream.Seek(-11, SeekOrigin.End);
        //使用XmlWriter写入
        using (XmlWriter writer = XmlWriter.Create(filestream, settings))
        {
            //写入2个空格，为了对齐
            writer.WriteWhitespace("  ");
            //写入元素头
            writer.WriteStartElement("renyuan");
            //写入属性"name"、"password"名和值
            writer.WriteAttributeString("name", user);
            writer.WriteAttributeString("password", passw);
            //写入元素结束标记
            writer.WriteEndElement();
            //写入换行
            writer.WriteWhitespace(System.Environment.NewLine); 
            //补写根元素结束标记       
            writer.WriteRaw("</renyuans>");
            writer.Close();
        }
        filestream.Close();//文件关闭
        //设置返回信息格式，文本
        context.Response.ContentType = "text/plain";
        //下面输出返到客户端e.Result中
        context.Response.Write("服务器端写入XML成功!写入信息是：" + user + "和" + passw); 
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}