
export interface LatestMessage {
  messageId: number;
  isRead: boolean;
  unreadMessagesCount: number;
  textContent: string;
  authorId: number;
  authorName: string;
  createdAt: Date;

  userRecipient: RecipientUser;
  groupRecipient: RecipientGroup;
}

interface RecipientUser {
  userId: number;
  displayName: string;
  isOnline?: string;
}

interface RecipientGroup {
  groupId: number;
  name: string;
}
