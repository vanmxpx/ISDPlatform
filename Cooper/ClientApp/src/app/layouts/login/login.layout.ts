import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SocialNetwork } from '@enums';
import { AuthentificationService, LocalizationService } from '@services';
import { TranslateService } from '@ngx-translate/core';
import { MatDialog } from '@angular/material/dialog';
import { SocialRegisterComponent } from '@components';

@Component({
  selector: 'coop-login-layout',
  templateUrl: './login.layout.html',
  styleUrls: ['./login.layout.css']
})
export class LoginLayoutComponent implements OnInit {

  public failedLogin: boolean = false;
  public languageKeys: string[] = this.localizationService.languageKeys;
  public languages: any = this.localizationService.languages;
  public currentLanguage: string = this.localizationService.getCurrentLanguage();

  constructor(private authService: AuthentificationService, private route: ActivatedRoute,
              private router: Router, public translate: TranslateService, private localizationService: LocalizationService,
              public dialog: MatDialog) { }

  public ngOnInit(): void {
    this.route.params.subscribe((params) => {
      console.log(params);
      if (params.failedLogin) {
        this.failedLogin = params.failedLogin;
      }
    });

    if (this.authService.isAuthentificated()) {
      this.router.navigate(['/platform/home']);
    }

    this.authService.registerEvent.subscribe(
      (userData) => {
        const dialogRef = this.dialog.open(SocialRegisterComponent);
        dialogRef.afterClosed().subscribe(
          (nickname) => {
            if (nickname !== '') {
              this.authService.socialRegister(userData, nickname);
            }
          });
      }
    );
  }

  public signIn(form: NgForm): void {
    this.authService.signIn(JSON.stringify(form.value));
  }

  public socialSignIn(platform: SocialNetwork): void {
    this.authService.socialSignIn(platform);
  }

  public onLangChanged(lang: string): void {
    this.localizationService.onLanguageChanged(lang);
  }
  public setDefaultLang(): void {
    this.translate.setDefaultLang(this.currentLanguage);
  }
}
