
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
