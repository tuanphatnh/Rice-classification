import { Link, useRouteError } from 'react-router-dom';

export default function ErrorPage() {
  const error = useRouteError();

  return (
    <section className="page_404">
      <div className="four_zero_four_bg">
        <h1 className="title">{error.statusText}</h1>
      </div>
      <p></p>
      <div className="contant_box_404">{error.message}
      <Link to={"/"} className="link_404">Go Admin Page</Link>
      </div>

      
    </section>
  );
}
