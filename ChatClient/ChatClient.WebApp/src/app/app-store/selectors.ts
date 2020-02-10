import { createSelector } from '@ngrx/store';
import { AuthStoreSelectors } from './auth-store';
import { LatestMessagesStoreSelectors } from './latest-messages-store';

export const selectLoading = createSelector(
  AuthStoreSelectors.selectLoading,
  LatestMessagesStoreSelectors.selectLoading,
  (...errors) => errors.some(Boolean),
);

export const selectError = createSelector(
  AuthStoreSelectors.selectError,
  LatestMessagesStoreSelectors.selectError,
  (...errors) => errors.filter(Boolean),
);
