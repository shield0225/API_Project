using System;
using System.Collections.Generic;

namespace ChatLibrary.Models;

public partial class Chatroom
{
    public int ChatroomId { get; set; }

    public int AccessibleUserId { get; set; }

    public virtual User AccessibleUser { get; set; } = null!;

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
}
