import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';  
import { AuthService } from '../auth/auth.service';

@Directive({ selector: '[hasPermission]'})
export class HasPermissionDirective {
  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private authService: AuthService) { }

  @Input() set hasPermission(permission: string) {  
    this.authService.checkPermission(permission)
                     .subscribe(response => 
                     { 
                           if(response.isSuccess)
                           {
                              this.viewContainer.createEmbeddedView(this.templateRef);
                           }
                           else
                           {
                              this.viewContainer.clear();
                           }
                     },
                     () => {
                           this.viewContainer.clear();
                     });
  }
}