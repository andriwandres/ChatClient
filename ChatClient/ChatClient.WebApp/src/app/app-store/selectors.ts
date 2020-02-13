import { createSelector } from '@ngrx/store';
import { AuthStoreSelectors } from './auth-store';
import { LatestMessagesStoreSelectors } from './latest-messages-store';

export const selectLoading = createSelector(
  AuthStoreSelectors.selectLoading,
  LatestMessagesStoreSelectors.selectLoading,
  (...indicators) => indicators.some(Boolean),
);

export const selectError = createSelector(
  AuthStoreSelectors.selectError,
  LatestMessagesStoreSelectors.selectError,
  (...errors) => errors.filter(Boolean),
);
