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
import HomeCarousel from "../components/HomeCarousel";

const Home = () => {
  return (
    <div>
      <HomeCarousel />
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
