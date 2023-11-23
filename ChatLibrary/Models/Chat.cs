using System;
using System.Collections.Generic;

namespace ChatLibrary.Models;

public partial class Chat
{
    public int ChatId { get; set; }

    public int ChattedUserId { get; set; }

    public int ChatroomId { get; set; }

    public DateTime Date { get; set; }

    public string? ChatContent { get; set; }

    public string? Path { get; set; }

    public virtual Chatroom Chatroom { get; set; } = null!;

    public virtual User ChattedUser { get; set; } = null!;
}
