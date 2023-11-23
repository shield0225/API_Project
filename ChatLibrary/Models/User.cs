using System;
using System.Collections.Generic;

namespace ChatLibrary.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Chatroom> Chatrooms { get; set; } = new List<Chatroom>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
}
