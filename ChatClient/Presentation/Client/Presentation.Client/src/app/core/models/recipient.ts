import { AvailabilityStatus } from './availability-status';
import { TargetGroup } from './group';
import { LatestMessage } from './message';
import { TargetUser } from './user';

export interface Recipient {
  recipientId: number;
  availabilityStatus?: AvailabilityStatus;
  unreadMessagesCount: number;
  isPinned: boolean;
  targetUser?: TargetUser;
  targetGroup?: TargetGroup;
  latestMessage: LatestMessage;
}
