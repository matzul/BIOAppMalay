using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for ToDoItem
/// </summary>
[DataContract]
public class ToDoItem
{
    [DataMember]
    public Guid Id { get; set; }

    [DataMember]
    public string Description { get; set; }
}