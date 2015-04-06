using System;
using System.Collections.Generic;
using System.Text;
using COM.SingNo.Common;
namespace COM.SingNo.CMS.Core
{
   public class DataModelManager
    {
      private static volatile DataModelManager _instance;
      private static object syncRoot = new Object();
      private static SafeDictionary<int, DataModel> _DataModelColls;
      private DataModelManager()
      {
          _DataModelColls = new SafeDictionary<int, DataModel>();
      }
      public static DataModelManager createInstance()
      {
          if (_instance == null)
          {
              lock (syncRoot)
              {
                  if (_instance == null) _instance = new DataModelManager();
              }
          }
          return _instance;
      }
      public void addModel(int modelId,DataModel model)
      {
          if(_DataModelColls.ContainsKey(modelId))
          {
                _DataModelColls[modelId]=model;
          }else
          {
              _DataModelColls.Add(modelId,model);
          }
      }
      public void clear()
      {
          _DataModelColls.Clear();
      }
      public void removeModel(int modelId)
      {
          _DataModelColls[modelId] = null;
          _DataModelColls.Remove(modelId);
      }
      public int getCount()
      {
          return _DataModelColls.Count;
      }
       public DataModel getModel(int modelId)
       {
           return _DataModelColls[modelId];
       }
    }
    public class DataModel
    {
      public int ModelId{get;set;}
      public string ModelName { get; set; }
      public string TableName { get; set; }
      public string ItemName { get; set; }
      public string ItemUnit { get; set; }
      public string ItemIcon { get; set; }
      public int State { get; set; }
      //public int UseNumber { get; set; }
      private SafeList<ModelField> _fieldList;
      public SafeList<ModelField> fieldList
      {
          get
          {
              return _fieldList;
          }
      }
      public void addField(ModelField field)
      {
          if(_fieldList.Contains(field))
          {
              int index = _fieldList.IndexOf(field);
              _fieldList[index] = field;
          }else{
              _fieldList.Add(field);
          }
      }
      public void clear()
      {
          _fieldList.Clear();
      }
      public void removeByName(string name)
      {
          foreach (ModelField field in _fieldList)
          {
              if (string.Compare(field.FieldName,name,true)==0)_fieldList.Remove(field);
              break;
          }
      }
      //public void removeByIndex(int index)
      //{
      //    foreach (ModelField field in _fieldList)
      //    {
      //        if (field.IndexId == index) _fieldList.Remove(field);
      //    }
      //}
      public ModelField getFieldByName(string name)
      {
          foreach (ModelField field in _fieldList)
          {
              if (string.Compare(field.FieldName, name, true) == 0) return field;
          }
          return null;
      }
      //public ModelField getFieldByIndex(int index)
      //{
      //    foreach (ModelField field in _fieldList)
      //    {
      //        if (field.IndexId == index) return field;
      //    }
      //    return null;
      //}
      public int getFieldCount()
      {
          return _fieldList.Count;
      }
      public DataModel()
      {
          _fieldList = new SafeList<ModelField>();
      }
    }
   public class ModelField
   {
       public string FieldName { get; set; }
       public bool isVisible { get; set; }
   }
}
