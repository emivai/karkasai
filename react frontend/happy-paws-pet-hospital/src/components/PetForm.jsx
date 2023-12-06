import SelectDropdown from "../components/SelectDropdown";

const PetForm = ({ onChange, formValues }) => {
  function handleChange(v) {
    const { id, value } = v.target;
    onChange(id, value);
  }

  function handleNumericChange(v) {
    const { id, value } = v.target;
    onChange(id, Number(value));
  }

  function handleDateChange(v) {
    const { id, value } = v.target;
    onChange(id, new Date(value).toISOString());
  }

  return (
    <form className="my-4">
      <SelectDropdown
        header={"Type"}
        formId="type"
        options={["Dog", "Cat", "Rodent", "Exotic"]}
        value={formValues.type ?? 0}
        handleChange={handleNumericChange}
      />
      <label className="sr-only">Name</label>
      <input
        type="text"
        id="name"
        className="form-control"
        placeholder="Enter pet's name"
        required
        value={formValues.name ?? ""}
        onChange={(e) => handleChange(e)}
      />
      <label>BirthDate</label>
      <input
        type="date"
        id="birthdate"
        className="form-control"
        value={new Date(formValues.birthdate).toLocaleDateString("lt-LT")}
        onChange={(e) => handleDateChange(e)}
      />
      <label className="sr-only">Photo</label>
      <input
        type="text"
        id="photo"
        className="form-control"
        placeholder="Photo url"
        required
        value={formValues.photo ?? ""}
        onChange={(e) => handleChange(e)}
      />
    </form>
  );
};

export default PetForm;
