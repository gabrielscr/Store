import OIDC from 'oidc-client';
import env from '../env/env';

OIDC.Log.logger = console;

const authService: OIDC.UserManager = new OIDC.UserManager({
  authority: env.auth.authority,
  client_id: env.auth.client_id,
  redirect_uri: env.auth.redirect_uri,
  response_type: "id_token token",
  scope: "openid profile dna-app-api",
  post_logout_redirect_uri: env.auth.redirect_uri,
  revokeAccessTokenOnSignout: true
});

authService.events.addUserLoaded(function (user) {
  setUser(user);
});

export { authService };

const TOKEN_KEY = env.auth.client_id + '-token';

export function getToken() {
  let user = window.localStorage.getItem(TOKEN_KEY);

  if (user)
    return JSON.parse(user);

  return null;
}

export function setUser(user) {
  window.localStorage.setItem(TOKEN_KEY, JSON.stringify(user));
}
