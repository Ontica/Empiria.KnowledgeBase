/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Help Desk                                    Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Partitioned type                      *
*  Type     : Ticket                                       License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Handles information about a help desk ticket.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.Contacts;

using Empiria.Ontology;

namespace Empiria.HelpDesk {

  /// <summary>Handles information about a help desk ticket.</summary>
  [PartitionedType(typeof(TicketType))]
  public class Ticket : BaseObject {

    #region Constructors and parsers

    protected Ticket(TicketType powerType) : base(powerType) {
      // Required by Empiria Framework for all partitioned types.
    }


    public Ticket(JsonObject data) {
      this.AssertIsValid(data);

      this.Load(data);
    }


    static internal Ticket Parse(int id) {
      return BaseObject.ParseId<Ticket>(id);
    }


    static public Ticket Parse(string uid) {
      return BaseObject.ParseKey<Ticket>(uid);
    }


    static public FixedList<Ticket> Search(string keywords) {
      return HelpDeskData.SearchTickets(keywords);
    }

    public static FixedList<Ticket> GetOpened(string keywords) {
      return HelpDeskData.GetOpenedTickets(keywords);
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


    [DataField("Title")]
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


    internal JsonObject ExtensionData {
      get;
      private set;
    } = new JsonObject();


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.ControlNo, this.Title, this.Tags, this.Description);
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


    [DataField("Status", Default = ObjectStatus.Active)]
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


    //public FixedList<Topic> Topics {
    //  get {
    //    return new FixedList<Topic>();
    //  }
    //}


    //public FixedList<Recommendation> Recommendations {
    //  get {
    //    return new FixedList<Topic>();
    //  }
    //}


    //public FixedList<Agreement> Agreements {
    //  get {
    //    return new FixedList<Agreement>();
    //  }
    //}

    #endregion Public properties

    #region Public methods

    public void Delete() {
      this.Status = ObjectStatus.Deleted;

      this.Save();
    }


    protected override void OnSave() {
      if (this.UID.Length == 0) {
        this.UID = EmpiriaString.BuildRandomString(6, 36);
        this.Customer = Contact.Parse(51);
        this.Provider = Contact.Parse(50);
      }
      HelpDeskData.WriteTicket(this);
    }


    public void Update(JsonObject data) {
      Assertion.AssertObject(data, "data");

      this.AssertIsValid(data);

      this.Load(data);

      this.Save();
    }

    #endregion Public methods

    #region Private methods

    private void AssertIsValid(JsonObject data) {
      Assertion.AssertObject(data, "data");

    }


    private void Load(JsonObject data) {
      this.ControlNo = data.Get<string>("controlNo", this.ControlNo);
      this.Title = data.Get<string>("title", this.Title);
      this.Description = data.Get<string>("description", this.Description);
    }

    #endregion Private methods

  }  // class Ticket

}  // namespace Empiria.HelpDesk
