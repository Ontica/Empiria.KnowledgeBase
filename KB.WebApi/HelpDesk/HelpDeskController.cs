/* Empiria Knowledge Base ************************************************************************************
*                                                                                                            *
*  Module   : Help Desk                                    Component : Web Api                               *
*  Assembly : Empiria.KnowledgeBase.WebApi.dll             Pattern   : Controller                            *
*  Type     : HelpDeskController                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API controller for help desk tickets and their related entities.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;

namespace Empiria.HelpDesk.WebApi {

  /// <summary> Web API controller for help desk tickets and their related entities.</summary>
  public class HelpDeskController : WebApiController {

    #region GET methods

    [HttpGet]
    [Route("v1/help-desk/tickets/{ticketUID}")]
    public SingleObjectModel GetTicket(string ticketUID = "") {
      try {
        var ticket = Ticket.Parse(ticketUID);

        return new SingleObjectModel(this.Request, ticket.ToResponse(),
                                     typeof(Ticket).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpGet]
    [Route("v1/help-desk/tickets/opened")]
    public CollectionModel GetOpenedTickets([FromUri] string keywords = "") {
      try {

        FixedList<Ticket> ticketsList = Ticket.GetOpened<Ticket>(keywords);

        return new CollectionModel(this.Request, ticketsList.ToResponse(),
                                   typeof(Ticket).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpGet]
    [Route("v1/help-desk/tickets")]
    public CollectionModel SearchTickets([FromUri] string keywords = "") {
      try {

        FixedList<Ticket> ticketsList= Ticket.Search<Ticket>(keywords);

        return new CollectionModel(this.Request, ticketsList.ToResponse(),
                                   typeof(Ticket).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion GET methods

    #region UPDATE methods

    [HttpPost]
    [Route("v1/help-desk/tickets")]
    public SingleObjectModel CreateTicket([FromBody] object body) {
      try {
        base.RequireBody(body);

        var bodyAsJson = JsonObject.Parse(body);

        var ticketType = TicketType.Parse(bodyAsJson.Get<string>("type"));

        Ticket ticket = ticketType.CreateInstance(bodyAsJson);

        ticket.Save();

        return new SingleObjectModel(this.Request, ticket.ToResponse(),
                                     typeof(Ticket).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v1/help-desk/tickets/{ticketUID}/close")]
    public SingleObjectModel CloseTicket(string ticketUID) {
      try {

        var ticket = Ticket.Parse(ticketUID);

        ticket.Close();

        ticket.Save();

        return new SingleObjectModel(this.Request, ticket.ToResponse(),
                                     typeof(Ticket).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v1/help-desk/tickets/{ticketUID}")]
    public SingleObjectModel UpdateTicket(string ticketUID, [FromBody] object body) {
      try {
        base.RequireBody(body);

        var bodyAsJson = JsonObject.Parse(body);

        var ticket = Ticket.Parse(ticketUID);

        ticket.Update(bodyAsJson);

        ticket.Save();

        return new SingleObjectModel(this.Request, ticket.ToResponse(),
                                     typeof(Ticket).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpDelete]
    [Route("v1/help-desk/tickets/{ticketUID}")]
    public NoDataModel DeleteTicket(string ticketUID) {
      try {

        var ticket = Ticket.Parse(ticketUID);

        ticket.Delete();

        ticket.Save();

        return new NoDataModel(this.Request);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion UPDATE methods

  }  // class HelpDeskController

}  // namespace Empiria.HelpDesk.WebApi
