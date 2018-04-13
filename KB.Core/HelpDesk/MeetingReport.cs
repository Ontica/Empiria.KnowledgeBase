/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Help Desk                                    Component : Domain services                       *
*  Assembly : Empiria.KnowledgeBase.dll                    Pattern   : Partitioned type                      *
*  Type     : MeetingReport                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Handles information about a meeting.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.HelpDesk {

    /// <summary>Handles information about a meeting.</summary>
    public class MeetingReport : Ticket {

    #region Constructors and parsers

    private MeetingReport(TicketType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }
    public MeetingReport(JsonObject data) : base(TicketType.MeetingReport) {
      this.Load(data);
    }


    static public new MeetingReport Parse(string uid) {
      return BaseObject.ParseKey<MeetingReport>(uid);
    }

    #endregion Constructors and parsers

    #region Public properties

    public DateTime Date {
      get {
        return base.ExtensionData.Get("/meetingReport/date",
                                      ExecutionServer.DateMaxValue);
      }
      set {
        base.ExtensionData.SetIfValue("/meetingReport/date", value);
      }
    }


    public string Location {
      get {
        return base.ExtensionData.Get("/meetingReport/location",
                                      String.Empty);
      }
      set {
        base.ExtensionData.SetIfValue("/meetingReport/location", value);
      }
    }


    public string StartTime {
      get {
        return base.ExtensionData.Get("/meetingReport/startTime",
                                      String.Empty);
      }
      set {
        base.ExtensionData.SetIfValue("/meetingReport/startTime", value);
      }
    }


    public string EndTime {
      get {
        return base.ExtensionData.Get("/meetingReport/endTime",
                                      String.Empty);
      }
      set {
        base.ExtensionData.SetIfValue("/meetingReport/endTime", value);
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

    public override void Update(JsonObject data) {
      this.AssertIsValid(data);

      this.Load(data);
    }

    #endregion Public methods

    #region Private methods

    protected override void AssertIsValid(JsonObject data) {
      base.AssertIsValid(data);
    }


    protected override void Load(JsonObject data) {
      base.Load(data);

      this.Date = data.Get("date", this.Date);
      this.StartTime = data.Get("startTime", this.StartTime);
      this.EndTime = data.Get("endTime", this.EndTime);

      this.Location = data.GetClean("location", this.Location);
    }

    #endregion Private methods

  }  // class MeetingReport

}  // namespace Empiria.HelpDesk
