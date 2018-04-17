/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Help Desk                                    Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Partitioned type                      *
*  Type     : HelpDeskTicket                               License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Abstract classs that handles information about a help desk ticket.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.Contacts;

using Empiria.Ontology;

namespace Empiria.HelpDesk {

  /// <summary>Abstract classs that handles information about a help desk ticket.</summary>
  [PartitionedType(typeof(TicketType))]
  abstract public class HelpDeskTicket : BaseObject {

    #region Constructors and parsers

    protected HelpDeskTicket(TicketType powerType) : base(powerType) {
      // Required by Empiria Framework for all partitioned types.
    }


    static internal HelpDeskTicket Parse(int id) {
      return BaseObject.ParseId<HelpDeskTicket>(id);
    }


    static public HelpDeskTicket Parse(string uid) {
      return BaseObject.ParseKey<HelpDeskTicket>(uid);
    }


    static public FixedList<T> Search<T>(string keywords) where T: HelpDeskTicket {
      return HelpDeskData.SearchTickets<T>(keywords);
    }


    static public FixedList<T> GetOpened<T>(string keywords) where T : HelpDeskTicket {
      return HelpDeskData.GetOpenedTickets<T>(keywords);
    }

    #endregion Constructors and parsers

    #region Public properties

    public TicketType TicketType {
      get {
        return (TicketType) base.GetEmpiriaType();
      }
    }


    [DataField("UID")]
    public string UID {
      get;
      private set;
    } = String.Empty;


    [DataField("CustomerId")]
    public Contact Customer {
      get;
      private set;
    }


    [DataField("ProviderId")]
    public Contact Provider {
      get;
      private set;
    }


    [DataField("ControlNo")]
    public string ControlNo {
      get;
      private set;
    } = String.Empty;


    [DataField("Title", Default = "No asignado")]
    public string Title {
      get;
      private set;
    } = String.Empty;


    [DataField("Description")]
    public string Description {
      get;
      private set;
    } = String.Empty;


    [DataField("Tags")]
    public string Tags {
      get;
      private set;
    } = String.Empty;


    [DataField("ExtData")]
    protected internal JsonObject ExtensionData {
      get;
      private set;
    } = new JsonObject();


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.ControlNo, this.Title,
                                           this.Tags, this.Description);
      }
    }


    [DataField("RequestedTime", Default = "DateTime.Now")]
    public DateTime RequestedTime {
      get;
      private set;
    }


    [DataField("AssignedToId")]
    public Contact AssignedTo {
      get;
      private set;
    }


    [DataField("ResolutionTime", Default = "ExecutionServer.DateMaxValue")]
    public DateTime ResolutionTime {
      get;
      private set;
    }


    [DataField("Status", Default = ObjectStatus.Pending)]
    public ObjectStatus Status {
      get;
      private set;
    }


    public FixedList<Contact> Participants {
      get {
        var list = new Contact[2];

        list[0] = Contact.Parse(8);
        list[1] = Contact.Parse(10);

        return new FixedList<Contact>(list);
      }
    }

    #endregion Public properties

    #region Public methods

    public void Close() {
      this.Status = ObjectStatus.Closed;
      this.ResolutionTime = DateTime.Now;
    }


    public void Delete() {
      this.Status = ObjectStatus.Deleted;
    }


    protected override void OnBeforeSave() {
      if (this.IsNew) {
        this.UID = EmpiriaString.BuildRandomString(6, 36);

        if (this.Customer.IsEmptyInstance) {
          this.Customer = Contact.Parse(51);
        }

        if (this.Provider.IsEmptyInstance) {
          this.Provider = Contact.Parse(50);
        }

        this.ControlNo = HelpDeskData.GetNextControlNoForTicket(this.Provider);
      }
    }


    protected override void OnSave() {
      HelpDeskData.WriteTicket(this);
    }


    public virtual void Update(JsonObject data) {
      this.AssertIsValid(data);

      this.Load(data);
    }

    #endregion Public methods

    #region Private methods

    protected virtual void AssertIsValid(JsonObject data) {
      Assertion.AssertObject(data, "data");

    }

    protected virtual void Load(JsonObject data) {
      this.Customer = data.Get<Contact>("customerUID", this.Customer);
      this.Provider = data.Get<Contact>("providerUID", this.Provider);
      this.AssignedTo = data.Get<Contact>("assignedToUID", this.AssignedTo);

      this.Title = data.GetClean("title", this.Title);
      this.Description = data.GetClean("description", this.Description);
      this.Tags = data.GetClean("tags", this.Tags);

    }

    #endregion Private methods

  }  // class HelpDeskTicket

}  // namespace Empiria.HelpDesk
