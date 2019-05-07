import { HttpClient, HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

export class CooperInterceptor implements HttpInterceptor{
    intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{       

        if(localStorage.getItem("jwtCooper")){
            const cloneRequest = req.clone()
            headers: req.headers.set ('Authorization', `Bearer ${localStorage.getItem("jwtCooper")}`)     
            return next.handle(cloneRequest)  
        }
        return next.handle(req)
    }

}