import { Fragment } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { publicRoutes } from '~/routes';
import LayoutAdmin from './pages/adminPage/LayoutAdmin';
import ErrorPage from './ErrorPage';
import CheckReport from './pages/adminPage/CheckReport';
import Size from './pages/adminPage/Size';
import Category from './pages/adminPage/Category';
import CoinPackage from './pages/adminPage/CoinPackage';
import Brand from './pages/adminPage/Brand';
import ReportCategory from './pages/adminPage/ReportCategory';
import SubcriptionPackage from './pages/adminPage/SubcriptionPackage';
import Approve from './pages/adminPage/Approve';
import PostStatistic from './pages/adminPage/PostStatistic';
import ReportStatistic from './pages/adminPage/ReportStatistic';
import Role from './pages/adminPage/Role';
import AssignRole from './pages/adminPage/AssignRole';
import DashBoard from './pages/adminPage/DashBoard';
import Color from './pages/adminPage/Color';
import User from './pages/adminPage/Users';

function App() {
    return (
        <Router>
            <Routes>
                {publicRoutes.map((route, index) => {
                    let Layout = Fragment;

                    if (route.layout) {
                        Layout = route.layout;
                    }

                    const Page = route.component;
                    return (
                        <Route
                            key={index}
                            path={route.path}
                            element={
                                <Layout>
                                    <Page />
                                </Layout>
                            }
                        />
                    );
                })}
                 <Route path="/admin" element={<LayoutAdmin />} errorElement={<ErrorPage />}>
      <Route
        path="/admin/dashboard"
        element={<DashBoard />}
        errorElement={<ErrorPage />}></Route>
         <Route
        path="/admin/check-report"
        element={<CheckReport />}
        errorElement={<ErrorPage />}></Route>
      <Route
        path="/admin/business/size"
        element={<Size />}
        errorElement={<ErrorPage />}></Route>
      <Route
        path="/admin/business/category"
        element={<Category />}
        errorElement={<ErrorPage />}></Route>
      <Route
        path="/admin/business/coin-package"
        element={<CoinPackage />}
        errorElement={<ErrorPage />}></Route>
         <Route
        path="/admin/business/brand"
        element={<Brand />}
        errorElement={<ErrorPage />}></Route>
        <Route
        path="/admin/business/report-category"
        element={<ReportCategory />}
        errorElement={<ErrorPage />}></Route>
         <Route
        path="/admin/business/subscription-package"
        element={<SubcriptionPackage />}
        errorElement={<ErrorPage />}></Route>
        <Route
        path="/admin/business/color"
        element={<Color />}
        errorElement={<ErrorPage />}></Route>
      <Route
        path="/admin/approve"
        element={<Approve />}
        errorElement={<ErrorPage />}></Route>
        <Route
        path="/admin/users"
        element={<User />}
        errorElement={<ErrorPage />}></Route>
      <Route
        path="/admin/statistic/post"
        element={<PostStatistic />}
        errorElement={<ErrorPage />}></Route>
         <Route
        path="/admin/statistic/report"
        element={<ReportStatistic />}
        errorElement={<ErrorPage />}></Route>
           <Route
        path="/admin/role"
        element={<Role />}
        errorElement={<ErrorPage />}></Route>
         <Route
        path="/admin/assign-role"
        element={<AssignRole />}
        errorElement={<ErrorPage />}></Route>
           <Route
        path="/admin/role"
        element={<Role />}
        errorElement={<ErrorPage />}></Route>
    </Route>
            </Routes>
       
        </Router>
    );
}

export default App;
