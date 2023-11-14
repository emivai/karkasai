import React, { useState } from "react";
import Logo1 from "../images/vf-issuelanding-vet-checking-heartbeat-of-brown-and-white-cat.jpg";
import Logo2 from "../images/logo-white.png";
import Logo3 from "../images/2.jpg";
import Logo4 from "../images/ODAH19_med-89_-_2.webp";
import Logo5 from "../images/pet-surgery-dog-cat-spruce-grove.jpg";
import Logo6 from "../images/5d289d6921a86120285e5e24.webp";
import Logo7 from "../images/Mr-Potts_Grooming-Cavoodle.jpg";

import { Link } from "react-router-dom";
import Featurette from "../components/Featurette";

const Home = () => {
  const [index, setIndex] = useState(0);

  const handlePreviousClick = () => {
    setIndex(index === 0 ? 2 : index - 1);
  };

  const handleNextClick = () => {
    setIndex(index === 2 ? 0 : index + 1);
  };

  return (
    <div>
      <div id="myCarousel" className="carousel slide" data-ride="carousel">
        <div className="carousel-indicators">
          <button
            type="button"
            data-bs-target="#myCarousel"
            onClick={() => setIndex(0)}
            className={index === 0 ? "active" : ""}
          ></button>
          <button
            type="button"
            data-bs-target="#myCarousel"
            onClick={() => setIndex(1)}
            className={index === 1 ? "active" : ""}
          ></button>
          <button
            type="button"
            data-bs-target="#myCarousel"
            onClick={() => setIndex(2)}
            className={index === 2 ? "active" : ""}
          ></button>
        </div>
        <div className="carousel-inner">
          <div className={`carousel-item ${index === 0 ? "active" : ""}`}>
            <img
              className="first-slide image darken"
              src={Logo1}
              alt="First slide"
            />
            <div className="container">
              <div className="carousel-caption text-left">
                <img
                  className="mb-4"
                  src={Logo2}
                  alt=""
                  width="200"
                  height="200"
                />
                <h1>Happy Paws Pet Hospital</h1>
                <p>We care for pets when they need it most.</p>
                <p>
                  <Link to="/signin">
                    <button className="btn btn-lg btn-info">Join today</button>
                  </Link>
                </p>
              </div>
            </div>
          </div>
          <div className={`carousel-item ${index === 1 ? "active" : ""}`}>
            <img
              className="second-slide image darken"
              src={Logo3}
              alt="Second slide"
            />
            <div className="container">
              <div className="carousel-caption">
                <h1>Exceptional staff</h1>
                <p>
                  We are committed to upholding the highest medical standards
                  and are constantly searching for new and better ways to treat
                  our patients.
                </p>
                <p>
                  <Link to="/doctors">
                    <button className="btn btn-lg btn-info">
                      Meet our team
                    </button>
                  </Link>
                </p>
              </div>
            </div>
          </div>
          <div className={`carousel-item ${index === 2 ? "active" : ""}`}>
            <img
              className="third-slide image darken"
              src={Logo4}
              alt="Third slide"
            />
            <div className="container">
              <div className="carousel-caption text-right">
                <h1>Industry-leading pet care</h1>
                <p>
                  Happy Paws stays on top of the latest advances in veterinarian
                  technology and above all, remembers that all animals and pets
                  need to be treated with loving care in every check-up,
                  procedure, or surgery.
                </p>
                <p>
                  <Link to="/prices">
                    <button className="btn btn-lg btn-info">Learn more</button>
                  </Link>
                </p>
              </div>
            </div>
          </div>
        </div>
        <button
          className="carousel-control-prev"
          type="button"
          onClick={() => handlePreviousClick()}
        >
          <span
            className="carousel-control-prev-icon"
            aria-hidden="true"
          ></span>
        </button>
        <button
          className="carousel-control-next"
          type="button"
          onClick={() => handleNextClick()}
        >
          <span
            className="carousel-control-next-icon"
            aria-hidden="true"
          ></span>
        </button>
      </div>

      <div className="container marketing">
        <Featurette
          header={"Pet Surgery"}
          description={
            "Our skilled group of surgeons performs your pet's surgeries with the highest precision and care. With a combined wealth of experience and a dedication to the well-being of animals, they approach each procedure with meticulous attention, ensuring the best possible outcomes. You can have peace of mind knowing that your pet's health and safety are our top priorities in every surgical endeavor."
          }
          btnText={"Book now"}
          imageSource={Logo5}
          order={0}
          linkTo={"/appointments"}
        />
        <Featurette
          header={"Veterinary Services"}
          description={
            "At our facility, we take pride in being a comprehensive, full-service medical and surgical hospital dedicated to offering a wide range of healthcare services for your pets. From routine check-ups to complex surgical procedures, our team is equipped with the expertise and state-of-the-art technology needed to provide the highest quality of care. We are committed to not only treating illnesses but also emphasizing the importance of preventative health care, ensuring your furry family members lead happy and healthy lives for years to come."
          }
          btnText={"Book now"}
          imageSource={Logo6}
          order={2}
          linkTo={"/appointments"}
        />
        <Featurette
          header={"Grooming"}
          description={
            "Our experts are dedicated to pampering your furry companions with soothing baths, stylish trims, nail care, and more, ensuring they not only look their best but also feel their best. Whether it's comprehensive medical care or a rejuvenating grooming session, our goal remains the same: to keep your pets happy, healthy, and radiant.At our facility, we take pride in being a comprehensive, full-service medical and surgical hospital dedicated to offering a wide range of healthcare services for your pets. From routine check-ups to complex surgical procedures, our team is equipped with the expertise and state-of-the-art technology needed to provide the highest quality of care. We are committed to not only treating illnesses but also emphasizing the importance of preventative health care, ensuring your furry family members lead happy and healthy lives for years to come."
          }
          btnText={"Book now"}
          imageSource={Logo7}
          order={0}
          linkTo={"/appointments"}
        />
      </div>
      <footer className="container d-flex justify-content-center">
        <p className="float-right">
          <a href="#">Back to top</a>
        </p>
      </footer>
    </div>
  );
};

export default Home;
