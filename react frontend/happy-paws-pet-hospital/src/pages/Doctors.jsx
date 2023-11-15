import React from "react";
import CirclePicture from "../components/CirclePicture";
import lilyAnderson from "../images/istockphoto-1330046035-612x612.jpg";
import davidWilliams from "../images/Dr.-Snead-e1678809328573.jpeg";
import sarahSmith from "../images/istockphoto-1136590917-640x640.jpg";
import annJones from "../images/depositphotos_389739044-stock-photo-profile-picture-of-female-doctor.jpg";
import bellaMoore from "../images/justinelee.jpg";
import liamHerwing from "../images/63ecfd25c727f_Lemon.png.webp";

const Doctors = () => {
  return (
    <div className="container marketing pt-5 d-flex flex-column">
      <div className="row">
        <CirclePicture title={"Lily Anderson"} imageSource={lilyAnderson} />
        <CirclePicture title={"David Williams"} imageSource={davidWilliams} />
        <CirclePicture title={"Sarah Smith"} imageSource={sarahSmith} />
        <CirclePicture title={"Ann Jones"} imageSource={annJones} />
        <CirclePicture title={"Bella Moore"} imageSource={bellaMoore} />
        <CirclePicture title={"Liam Herwing"} imageSource={liamHerwing} />
      </div>
    </div>
  );
};

export default Doctors;
