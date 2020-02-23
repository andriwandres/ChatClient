import { createSelector } from '@ngrx/store';
import { AuthStoreSelectors } from './auth-store';
import { LatestMessagesStoreSelectors } from './latest-messages-store';
import { ChatMessagesStoreSelectors } from './chat-messages-store';

// Selects if any Feature State is in Loading State
export const selectLoading = createSelector(
  AuthStoreSelectors.selectLoading,
  LatestMessagesStoreSelectors.selectLoading,
  ChatMessagesStoreSelectors.selectLoading,
  (...indicators) => indicators.some(Boolean),
);

// Selects all active Errors that have occured over all Feature States
export const selectError = createSelector(
  AuthStoreSelectors.selectError,
  LatestMessagesStoreSelectors.selectError,
  ChatMessagesStoreSelectors.selectError,
  (...errors) => errors.filter(Boolean),
);
