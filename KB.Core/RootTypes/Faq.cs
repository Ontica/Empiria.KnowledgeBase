/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Knowledge Base Entities                      Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Domain class                          *
*  Type     : Faq                                          License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents a (frequently) asked question.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.Contacts;
using Empiria.Security;
using Empiria.StateEnums;

namespace Empiria.KnowledgeBase {

  /// <summary>Represents a (frequently) asked question.</summary>
  public class Faq : BaseObject {

    #region Constructors and parsers

    protected Faq() {
      // Required by Empiria Framework.
    }


    public Faq(JsonObject data) {
      this.AssertIsValid(data);

      this.Load(data);
    }


    static internal Faq Parse(int id) {
      return BaseObject.ParseId<Faq>(id);
    }


    static public Faq Parse(string uid) {
      return BaseObject.ParseKey<Faq>(uid);
    }


    static public FixedList<Faq> Search(string keywords = "") {
      return KBItemsData.SearchFaq(keywords);
    }


    #endregion Constructors and parsers

    #region Public properties

    [DataField("UID")]
    public string UID {
      get;
      private set;
    } = String.Empty;


    [DataField("ControlNo")]
    public string ControlNo {
      get;
      private set;
    } = String.Empty;


    [DataField("Title")]
    public string Question {
      get;
      private set;
    } = String.Empty;


    [DataField("Body")]
    public string Answer {
      get;
      private set;
    } = String.Empty;


    [DataField("ExtData")]
    public string Comments {
      get;
      private set;
    } = String.Empty;


    [DataField("Tags")]
    public string Tags {
      get;
      private set;
    } = String.Empty;


    [DataField("OwnerId")]
    public Contact Owner {
      get;
      private set;
    }


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.ControlNo, this.Tags,
                                           this.Question, this.Answer);
      }
    }


    [DataField("AccessMode", Default = AccessMode.Public)]
    public AccessMode AccessMode {
      get;
      private set;
    }


    [DataField("ParentId", Default = -1)]
    internal int ParentId {
      get;
      private set;
    }


    [DataField("PostingTime", Default = "DateTime.Now")]
    public DateTime Date {
      get;
      private set;
    }


    [DataField("Status", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    public void Delete() {
      this.Status = EntityStatus.Deleted;

      this.Save();
    }


    protected override void OnSave() {
      if (this.UID.Length == 0) {
        this.UID = EmpiriaString.BuildRandomString(6, 24);
        this.Owner = EmpiriaUser.Current.AsContact();
      }
      KBItemsData.WriteFaq(this);
    }


    public void Update(JsonObject data) {
      Assertion.AssertObject(data, "data");

      this.AssertIsValid(data);

      this.Load(data);
    }

    #endregion Public methods

    #region Private methods

    private void AssertIsValid(JsonObject data) {
      Assertion.AssertObject(data, "data");

    }


    private void Load(JsonObject data) {
      this.Question = data.Get<string>("question", this.Question);
      this.Answer = data.Get<string>("answer", this.Answer);
      this.Comments = data.Get<string>("comments", this.Comments);
      this.AccessMode = data.Get<AccessMode>("accessMode", this.AccessMode);
      this.Status = data.Get<EntityStatus>("status", this.Status);
    }

    #endregion Private methods

  }  // class Faq

}  // namespace Empiria.KnowledgeBase
