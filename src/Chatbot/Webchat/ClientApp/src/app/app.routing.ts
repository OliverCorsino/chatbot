import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { DefaultLayoutComponent } from './layouts/default-layout/default-layout.component';

export const AppRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: DefaultLayoutComponent,
    children: [
      {
        path: '',
        component: HomeComponent
      }
    ]
  },
  {
    path: '**',
    component: HomeComponent
  }
];
