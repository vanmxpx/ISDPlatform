import {HttpInterceptor, HttpRequest, HttpHandler, HttpEvent} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@environment/environment';

export class CooperInterceptor implements HttpInterceptor {
    public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let reqWithNewUrl = req.clone({setHeaders: { 'Content-Type': 'application/json' }});
        if (!req.url.startsWith(environment.BASE_URL) && (!req.url.startsWith('/assets/i18n'))) {
            reqWithNewUrl = req.clone({ url: environment.BASE_URL + req.url });
        } else {
            reqWithNewUrl = req.clone();
        }

        const authToken = localStorage.getItem('JwtCooper');

        if (authToken) {
            const bearer = `Bearer ${authToken}`;
            const authReq = reqWithNewUrl.clone({ setHeaders: { Authorization: bearer } });
            return next.handle(authReq);
        }
        return next.handle(reqWithNewUrl);
    }

}
