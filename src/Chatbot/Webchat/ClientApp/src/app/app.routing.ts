import { Routes } from '@angular/router';
import { MessengerComponent } from './components/messenger/messenger.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { DefaultLayoutComponent } from './layouts/default-layout/default-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';

export const AppRoutes: Routes = [
  {
    path: '',
    component: DefaultLayoutComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        component: HomeComponent,
      },
      {
        path: 'chat-rooms/:chatroomId',
        canActivate: [AuthGuard],
        component: MessengerComponent,
      }
    ]
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: 'sign-up',
        component: SignUpComponent
      },
      {
        path: 'sign-in',
        component: LoginComponent
      }
    ]
  },
  {
    path: '**',
    component: LoginComponent
  }
];
