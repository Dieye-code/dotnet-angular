import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable()
export class ErrorRequestInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(
        (error: HttpErrorResponse) => {

          switch (error.status) {
            case 403:
              accessDeniedError();
              break;
            case 404:
              notFoundError();
              break;
            case 400:
              if (typeof (error.error) == "string")
                badRequestError(error.error);
              else
                badRequestError("Veuillez remplir correctement les champs")
              break;
            case 500:
              serverError()
              break;
            default:
          }
          throw error.message;
        }
      )
    );
  }
}


function notFoundError() {
  Swal.fire({
    title: "RESSOURCES INTROUVABLE",
    text: "La ressource spécifié est introuvable !",
    icon: "error",
  });
}
function serverError() {
  Swal.fire({
    title: "ERREUR SERVEUR",
    text: "Erreur au niveau du serveur",
    icon: "error",
  });
}
function accessDeniedError() {
  Swal.fire({
    title: "ACCES INTERDIT",
    text: "Vous n'avez pas le droit d'accéder à cette fonctionnalité",
    icon: "error",
  });
}
function badRequestError(message: any) {
  Swal.fire({
    title: "ERREUR PERSISTANT",
    text: message,
    icon: "error",
  });
}

