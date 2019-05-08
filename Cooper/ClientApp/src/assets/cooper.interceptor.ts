import { HttpClient, HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export class CooperInterceptor implements HttpInterceptor{
    intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{       
        console.log("ya tuta")
            console.log(localStorage.getItem("JwtCooper"))
            const authToken =localStorage.getItem("JwtCooper");
        if(authToken){
            const cloneRequest = req.clone()
            const bearer = `Bearer ${authToken}`
            console.log(bearer)
            const authReq = req.clone({ setHeaders: { Authorization: bearer } });
            console.log(authReq)
            return next.handle(authReq)  
        }
        return next.handle(req)
    }

}