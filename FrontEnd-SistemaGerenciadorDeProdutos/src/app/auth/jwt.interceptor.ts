import { HttpInterceptorFn } from '@angular/common/http';

function getAuthToken(): string | null {
  const isBrowser =
    typeof window !== 'undefined' && typeof localStorage !== 'undefined';

  if (isBrowser) {
    return localStorage.getItem('authToken');
  }

  console.warn(
    'JWT Interceptor: execução fora do navegador, localStorage não disponível.'
  );
  return null;
}

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const token = getAuthToken();

  if (token) {
    const clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });

    console.log('JWT Interceptor: Token anexado à requisição.');
    return next(clonedRequest);
  }

  console.warn(
    'JWT Interceptor: Token não encontrado, requisição enviada sem autenticação.'
  );
  return next(req);
};
