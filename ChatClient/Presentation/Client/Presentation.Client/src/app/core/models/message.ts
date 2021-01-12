
export interface LatestMessage {
  messageId: number;
  messageRecipientId: number;
  authorId: number;
  authorName: string;
  htmlContent: string;
  isRead: boolean;
  isOwnMessage: boolean;
  created: Date;
}

export interface ChatMessage {
  messageRecipientId: number;
  messageId: number;
  authorName: string;
  htmlContent: string;
  isRead: boolean;
  isOwnMessage: boolean;
  created: Date;
}
