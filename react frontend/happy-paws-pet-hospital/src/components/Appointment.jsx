import React, { useEffect, useState } from "react";

const Appointment = ({
  time,
  status = "Scheduled",
  petName,
  doctorName,
  procedures,
}) => {
  useEffect(() => {
    if (status === "Done") {
      setStatusStyle("success");
    } else if (status === "Cancelled") {
      setStatusStyle("danger");
    }
  }, []);

  const [statusStyle, setStatusStyle] = useState("primary");

  return (
    <div className="col-12 mt-3">
      <div className="card">
        <div className="card-body">
          <h5 className="card-title">{time}</h5>
          <p className={`badge bg-${statusStyle}`}>{status}</p>
          <p className="card-text">Pet: {petName}</p>
          <p className="card-text">Doctor: {doctorName}</p>
          <p className="card-text">Procedures: {procedures}</p>
          <div className="btn-toolbar d-flex gap-1" role="toolbar">
            <button type="button" className="btn btn-secondary mr-2 ">
              View
            </button>
            {status === "Scheduled" && (
              <>
                <button type="button" className="btn btn-secondary mr-2">
                  Edit
                </button>
                <button type="button" className="btn btn-danger mr-2">
                  Cancel
                </button>
              </>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Appointment;
