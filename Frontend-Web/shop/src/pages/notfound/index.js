import { Link } from 'react-router-dom';

function NotFoundPage() {
    return (
        <div className="flex justify-center items-center flex-col" style={{ marginTop: 100 }}>
            <h1 style={{ fontSize: 100 }}>404</h1>
            <h1 style={{ fontSize: 30 }}>Page Not Found</h1>
            <Link to="/home">
                <h1 style={{ fontSize: 24, color: 'blue' }}>Back to home page</h1>
            </Link>
        </div>
    );
}

export default NotFoundPage;
