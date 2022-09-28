using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using extendthirdPartyAPI;
using extendthirdPartyAPI.Models;
using extendthirdPartyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace extendthirdPartyAPI.Controllers;


/*
 *  Pay extend API integration controller. 
 *  
 * */
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Paginations))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorModel))]
    public async Task<IActionResult> GetCard()
    {
        String? authToken = null;

        try
        {
            authToken = ExtractAuthToken();
        

            _logger.LogInformation("Virtual card GET");
            Paginations result = await _connector.GetVirtualCards(HttpContext.Request.QueryString.ToString(), authToken);

            return Ok(result);
        }
        catch (ApiException ae)
        {
            return HandleException(ae);
        }catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("creditcard/{id}/transactions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transactions))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    public async Task<IActionResult> GetCardTransaction(String id)
    {
        String? authToken = null;


        try
        {
            authToken = ExtractAuthToken();
            _logger.LogInformation("GetTransactions, ID : " + id);
            Transactions result = await _connector.GetCardTransactions(id, HttpContext.Request.QueryString.ToString(), authToken);
            return Ok(result);
        }
        catch (ApiException ae)
        {
            return HandleException(ae);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("transactions/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transactions))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    public async Task<IActionResult> GetTransactionDetails(String id)
    {
        String? authToken = null;


        try
        {
            authToken = ExtractAuthToken();
            _logger.LogInformation("GetTransactionDetails, ID : " + id);
            TransactionDetails result = await _connector.GetTransactionDetails(id, authToken);
            return Ok(result);
        }
        catch (ApiException ae)
        {
            return HandleException(ae);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    private String ExtractAuthToken()
    {
        string authHeader = Request.Headers[HeaderNames.Authorization];

        if (string.IsNullOrEmpty(authHeader))
        {
            throw new ApiException { StatusCode = HttpStatusCode.Unauthorized, Message = "No auth header in request" };
        }


        string accessToken = authHeader.Substring("Bearer ".Length);

        if (string.IsNullOrEmpty(accessToken))
            throw new ApiException { StatusCode = HttpStatusCode.Unauthorized, Message = "Failed to extract token from auth header" };

        return accessToken;
    }

    private IActionResult HandleException(ApiException ae)
    {
        if (ae.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Unauthorized(new ErrorModel { ErrorMessage = ae.Message });
        } else if (ae.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound(new ErrorModel { ErrorMessage = ae.Message });
        } else if (ae.StatusCode == HttpStatusCode.BadRequest)
        {
            return BadRequest(new ErrorModel { ErrorMessage = ae.Message });
        }

        return StatusCode(StatusCodes.Status500InternalServerError, ae.Message);
    }

}
