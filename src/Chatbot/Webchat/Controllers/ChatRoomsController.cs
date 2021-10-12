using Core.Models;
using Core.Rules;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Immutable;

namespace Webchat.Controllers
{
    [Route("api/chat-rooms")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ChatRoomRules _chatRoomRules;

        /// <summary>
        /// Initializes a new instance of ChatRoomsController class.
        /// </summary>
        /// <param name="chatRoomRules">Represents the object that will handle the chat room business rule.</param>
        public ChatRoomsController(ChatRoomRules chatRoomRules) => _chatRoomRules = chatRoomRules;

        /// <summary>
        /// Retrieves all the chat rooms stored at the DB.
        /// </summary>
        /// <returns>A Collection with all the found education levels.</returns>
        [HttpGet("")]
        public ActionResult<IImmutableList<ChatRoom>> GetAll()
        {
            try
            {
                IImmutableList<ChatRoom> chatRooms = _chatRoomRules.GetAll();

                return Ok(chatRooms);
            }
            catch (Exception)
            {
                return BadRequest("There was an error processing your request. Please try again or contact our help desk.");
            }
        }
    }
}
