import React from "react";
import { Link } from "react-router-dom";

const Featurette = ({
  header,
  description,
  btnText,
  imageSource,
  order,
  linkTo,
}) => {
  return (
    <>
      <div className="row featurette">
        <div className={`col-md-7 order-${order}`}>
          <h2 className="featurette-heading">{header}</h2>
          <p className="lead">{description}</p>
          <p>
            <Link to={linkTo}>
              <button className="btn btn-secondary">{btnText} &raquo;</button>
            </Link>
          </p>
        </div>
        <div className="col-md-5 d-flex align-items-center">
          <img
            className="featurette-image img-fluid mx-auto"
            src={imageSource}
            alt="Pet surgery"
          />
        </div>
      </div>
      <hr className="featurette-divider" />
    </>
  );
};

export default Featurette;
