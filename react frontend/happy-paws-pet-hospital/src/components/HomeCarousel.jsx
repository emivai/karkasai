import Logo1 from "../images/vf-issuelanding-vet-checking-heartbeat-of-brown-and-white-cat.jpg";
import Logo2 from "../images/logo-white.png";
import Logo3 from "../images/2.jpg";
import Logo4 from "../images/ODAH19_med-89_-_2.webp";

import { Link } from "react-router-dom";
import React, { useState } from "react";

import Carousel from "react-bootstrap/Carousel";

const HomeCarousel = () => {
  const [index, setIndex] = useState(0);

  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };

  return (
    <Carousel activeIndex={index} onSelect={handleSelect}>
      <Carousel.Item>
        <img className="image darken" src={Logo1} alt="First slide" />
        <Carousel.Caption>
          {Logo2 && (
            <img className="mb-4" src={Logo2} alt="" width="200" height="200" />
          )}
          <h1>{"Happy Paws Pet Hospital"}</h1>
          <p>{"We care for pets when they need it most."}</p>
          <p>
            <Link to={"/signin"}>
              <button className="btn btn-lg btn-info">{"Join today"}</button>
            </Link>
          </p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img className="image darken" src={Logo3} alt="Second slide" />
        <Carousel.Caption>
          <h1>{"Exceptional staff"}</h1>
          <p>
            {
              "We are committed to upholding the highest medical standards and are constantly searching for new and better ways to treat our patients."
            }
          </p>
          <p>
            <Link to={"/doctors"}>
              <button className="btn btn-lg btn-info">{"Meet our team"}</button>
            </Link>
          </p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img className="image darken" src={Logo4} alt="Third slide" />
        <Carousel.Caption>
          <h1>{"Industry-leading pet care"}</h1>
          <p>
            {
              "Happy Paws stays on top of the latest advances in veterinarian technology and above all, remembers that all animals and pets need to be treated with loving care in every check-up, procedure, or surgery."
            }
          </p>
          <p>
            <Link to={"/prices"}>
              <button className="btn btn-lg btn-info">{"Learn more"}</button>
            </Link>
          </p>
        </Carousel.Caption>
      </Carousel.Item>
    </Carousel>
  );
};

export default HomeCarousel;
