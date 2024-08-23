// layout
import HomeLayout from '~/components/Layout/HomeLayout';
// page
import HomePage from '~/pages/home';
import LoginPage from '~/pages/auth/login';
import Register from '~/pages/auth/register';
import NewsPage from '~/pages/news';
import UserPage from '~/pages/user';
import AddressPage from '~/pages/address';
import Detailproduct from '~/pages/detailproduct/Detailproduct';
import PageNotFound from '~/pages/notfound';
import ChangePassword from '~/pages/changePasword';
import ForgotPassword from '~/pages/auth/Forget';
import ResetPassword from '~/pages/auth/reset';

const publicRoutes = [
    {
        path: '/',
        component: HomePage,
        layout: HomeLayout,
    },
    {
        path: '/home',
        component: HomePage,
        layout: HomeLayout,
    },
    {
        path: '/login',
        component: LoginPage,
        layout: null,
    },
    {
        path: '/forgot',
        component: ForgotPassword,
        layout: null,
    },
    {
        path: '/register',
        component: Register,
        layout: null,
    },
    {
        path: '/detail/:id',
        component: Detailproduct,
        layout: HomeLayout,
    },
    {
        path: '/user',
        component: UserPage,
        layout: HomeLayout,
    },
    {
        path: '/news',
        component: NewsPage,
        layout: HomeLayout,
    },
    {
        path: '/account/address',
        component: AddressPage,
        layout: HomeLayout,
    },
    {
        path: '/ResetPassword',
        component: ResetPassword,
        layout: null,
    },
    {
        path: '*',
        component: PageNotFound,
        layout: null,
    },
];

const privateRoutes = [];

export { publicRoutes, privateRoutes };
