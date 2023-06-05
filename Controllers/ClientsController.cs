using Microsoft.AspNetCore.Mvc;
using WebAppWithCRUD.dto.Request;
using WebAppWithCRUD.Models;
using WebAppWithCRUD.Services.Interfaces;

namespace WebAppWithCRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;

        private readonly IClientService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsController" /> class.
        /// </summary>
        /// <param name="service">The clients service.</param>
        /// <param name="logger">The controller logger.</param>
        public ClientsController(
            IClientService service,
            ILogger<ClientsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>Action result with the clients list.</returns>
        /// <response code="200">Successfully retrieved the list.</response>
        /// <response code="404">Bad request.</response>
        /// <response code="500">Internal error.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await this._service.GetAllAsync();

                if (!response.IsSuccessful)
                {
                    if (response.IsException)
                    {
                        return this.StatusCode(500, response.Exception);
                    }

                    return this.BadRequest(response.Errors);
                }

                return this.Ok(response.Payload);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $"ClientController.GetAllAsync: {e.Message}");
                return this.StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets client by id.
        /// </summary>
        /// <returns>Action result with the client.</returns>
        /// <response code="200">Successfully retrieved the client.</response>
        /// <response code="404">Bad request.</response>
        /// <response code="500">Internal error.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var response = await this._service.GetByIdAsync(id);

                if (!response.IsSuccessful)
                {
                    if (response.IsException)
                    {
                        return this.StatusCode(500, response.Exception);
                    }

                    return this.BadRequest(response.Errors);
                }

                return this.Ok(response.Payload);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $"ClientController.GetByIdAsync: {e.Message}");
                return this.StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Creates the new client.
        /// </summary>
        /// <returns>Action result with the client id.</returns>
        /// <response code="200">Successfully retrieved the client id.</response>
        /// <response code="404">Bad request.</response>
        /// <response code="500">Internal error.</response>
        [HttpPost]
        public async Task<IActionResult> PostAsync(InsertClientRequest request)
        {
            try
            {
                var response = await this._service.InsertBySPAsync(request);

                if (!response.IsSuccessful)
                {
                    if (response.IsException)
                    {
                        return this.StatusCode(500, response.Exception);
                    }

                    return this.BadRequest(response.Errors);
                }

                return this.Ok(response.Payload);
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $"ClientController.PostAsync: {e.Message}");
                return this.StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Delete the client by id.
        /// </summary>
        /// <returns>Action result.</returns>
        /// <response code="200">Successfully remove the client.</response>
        /// <response code="404">Bad request.</response>
        /// <response code="500">Internal error.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var response = await this._service.DeleteAsync(id);

                if (!response.IsSuccessful)
                {
                    if (response.IsException)
                    {
                        return this.StatusCode(500, response.Exception);
                    }

                    return this.BadRequest(response.Errors);
                }

                return this.Ok();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $"ClientController.DeleteAsync: {e.Message}");
                return this.StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <returns>Action result.</returns>
        /// <response code="200">Successfully update the client.</response>
        /// <response code="404">Bad request.</response>
        /// <response code="500">Internal error.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, UpdateClientRequest request)
        {
            try
            {
                var response = await this._service.UpdateBySPAsync(id, request);

                if (!response.IsSuccessful)
                {
                    if (response.IsException)
                    {
                        return this.StatusCode(500, response.Exception);
                    }

                    return this.BadRequest(response.Errors);
                }

                return this.Ok();
            }
            catch (Exception e)
            {
                this._logger.LogError(e, $"ClientController.PutAsync: {e.Message}");
                return this.StatusCode(500, e.Message);
            }
        }
    }
}