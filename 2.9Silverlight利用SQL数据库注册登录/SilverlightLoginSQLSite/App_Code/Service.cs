using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

//添加引用
using System.Collections.Generic;//for List<>
[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class Service
{
    [OperationContract]
    public List<zhuce> GetData()
    {
        //创建LINQ to SQL查询对象
        DataClassesDataContext dcdc = new DataClassesDataContext();
        //建立LINQ查询，选择zhuce表的所有记录
        var sqltable = from dc in dcdc.zhuce select dc;
        //返回数据集
        return sqltable.ToList<zhuce>();
    }
    [OperationContract]
    public string CheckName(string nm)
    {
        //创建LINQ to SQL查询对象
        DataClassesDataContext dcdc = new DataClassesDataContext();
        //建立LINQ查询，选择zhuce表的所有记录
        var check = from dc in dcdc.zhuce where dc.name == nm select dc;
        if (check.Count() != 0)
            return "Yes";//返回找到信息
        else
            return "No";//返回没有找到信息
    }
    [OperationContract]
    public string Tianjia(string nms,string pass)
    {
        DataClassesDataContext dcdc = new DataClassesDataContext();
        var records = from dc in dcdc.zhuce select dc;
        zhuce zhc = new zhuce()
           {
                id=records.Count()+1,
                name = nms,
                password = pass
            };
        //添加记录
        dcdc.zhuce.InsertOnSubmit(zhc);
        dcdc.SubmitChanges();
        return "注册成功";
    }
    [OperationContract]
    public string CheckPassword(string pw)
    {
        //创建LINQ to SQL查询对象
        DataClassesDataContext dcdc = new DataClassesDataContext();
        //建立LINQ查询，选择zhuce表的所有记录
        var check = from dc in dcdc.zhuce where dc.password == pw select dc;
        if (check.Count() != 0)
            return "Yes";//返回找到信息
        else
            return "No";//返回没有找到信息
    }
}
