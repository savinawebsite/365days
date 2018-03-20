<?php
require_once(dirname(__FILE__) . "/lib/php-liquid-0.9.2/Liquid.class.php");

class MuseDataPage {

    private $dir_name;

    public function __construct($dir_name) {
        $this->dir_name = $dir_name;
    }

    public function render($template_file, $data_files)
    {
        $liquid = new LiquidTemplate();
        $liquid->parse(file_get_contents($this->dir_name.'/'.$template_file));
        $assigns = array();
        foreach ($data_files as $data_file) {
            $data = json_decode(file_get_contents($this->dir_name.'/'.$data_file), true);
            $assigns = array_merge($assigns, $this->build_liquid_assigns($data));
        }
        $markup = $liquid->render($assigns);

        $show_editing_markup = isset($_GET["extendedit"]);
        if ( !$show_editing_markup )
        {
            $markup = MuseDataUtilities::removeEditingMarkup($markup);
        }

        return $markup;
    }

    private function build_liquid_assigns($data)
    {
        $assigns = array();
        foreach ($data as $record_key => $record) {
            $assigns[$record_key] = $record;
        }
        return $assigns;
    }
}

class MuseDataUtilities {

    public static function removeEditingMarkup($markup) {
        $patterns = array(
            "/^\\s*<!-- \\/?m_[^>]*-->\\s*$/m",
            "/<!-- \\/?m_[^>]*-->/m"
        );
        $markup = preg_replace($patterns, "", $markup);
        return $markup;
    }
}
?>