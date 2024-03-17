import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {

  public title: string = 'AdmissionDocsApp';

  public isCollapsed: boolean = true;
  public isLightTheme: boolean = false;

  public onThemeSwitch(): void {
    this.isLightTheme = !this.isLightTheme;
    document.body.setAttribute('data-bs-theme', this.isLightTheme ? 'light' : 'dark');
  }
}
