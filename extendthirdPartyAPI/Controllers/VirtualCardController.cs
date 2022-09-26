using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using extendthirdPartyAPI;
using extendthirdPartyAPI.Models;
using extendthirdPartyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace extendthirdPartyAPI.Controllers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

[ApiController]
[Route("[controller]")]
public class VirtualCardController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IPayExtendConnector _connector;

    public VirtualCardController(ILogger<VirtualCardController> logger, IPayExtendConnector connector)
    {
        this._logger = logger;
        this._connector = connector;
    }

    [HttpGet("creditcards")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCard()
    {
        _logger.LogInformation("Virtual card GET");
        Paginations result = await _connector.GetVirtualCards(HttpContext.Request.QueryString.ToString());

        return Ok(result);
    }

    [HttpGet("creditcard/{id}/transactions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(String))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCardTransaction(String id)
    {
        _logger.LogInformation("GetTransactions, ID : " + id);
        Transactions result = await _connector.GetCardTransactions(id, HttpContext.Request.QueryString.ToString());
        return Ok(result);
    }
}
